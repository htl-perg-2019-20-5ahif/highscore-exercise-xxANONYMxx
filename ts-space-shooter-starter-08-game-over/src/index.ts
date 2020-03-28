import { Scene, Types, CANVAS, Game, Physics, Input, GameObjects } from 'phaser';
import { Spaceship, Direction } from './spaceship';
import { Bullet } from './bullet';
import { Meteor } from './meteor';
import { request } from 'https';

/**
 * Space shooter scene
 * 
 * Learn more about Phaser scenes at 
 * https://photonstorm.github.io/phaser3-docs/Phaser.Scenes.Systems.html.
 */
class ShooterScene extends Scene {
    private spaceShip: Spaceship;
    private meteors: Physics.Arcade.Group;
    private bullets: Physics.Arcade.Group;
    private points: GameObjects.Text;

    private static counter = 0;
    private pName: string;

    private bulletTime = 0;
    private meteorTime = 0;

    private cursors: Types.Input.Keyboard.CursorKeys;
    private spaceKey: Input.Keyboard.Key;
    private isGameOver = false;

    preload() {
        // Preload images so that we can use them in our game
        this.load.image('space', 'images/deep-space.jpg');
        this.load.image('bullet', 'images/scratch-laser.png');
        this.load.image('ship', 'images/scratch-spaceship.png');
        this.load.image('meteor', 'images/scratch-meteor.png');
    }

    create() {
        if (this.isGameOver) {
            return;
        }
        
        //  Add a background
        this.add.tileSprite(0, 0, this.game.canvas.width, this.game.canvas.height, 'space').setOrigin(0, 0);

        this.points = this.add.text(this.game.canvas.width * 0.1, this.game.canvas.height * 0.1, "0", 
        { font: "32px Arial", fill: "#ff0044", align: "left" });

        // Create bullets and meteors
        this.bullets = this.physics.add.group({
            classType: Bullet,
            maxSize: 10,
            runChildUpdate: true
        });
        this.meteors = this.physics.add.group({
            classType: Meteor,
            maxSize: 20,
            runChildUpdate: true
        });

        // Add the sprite for our space ship.
        this.spaceShip = new Spaceship(this);
        this.physics.add.existing(this.children.add(this.spaceShip));

        // Position the spaceship horizontally in the middle of the screen
        // and vertically at the bottom of the screen.
        this.spaceShip.setPosition(this.game.canvas.width / 2, this.game.canvas.height * 0.9);

        // Setup game input handling
        this.cursors = this.input.keyboard.createCursorKeys();
        this.input.keyboard.addCapture([' ']);
        this.spaceKey = this.input.keyboard.addKey(Input.Keyboard.KeyCodes.SPACE);

        this.physics.add.collider(this.bullets, this.meteors, (bullet: Bullet, meteor: Meteor) => {
            this.points.setText((++ShooterScene.counter).toString())
            meteor.kill();
            bullet.kill();
            ShooterScene.counter ++;
        }, null, this);
        this.physics.add.collider(this.spaceShip, this.meteors, this.gameOver, null, this);
    }

    update(_, delta: number) {
        // Move ship if cursor keys are pressed
        if (this.cursors.left.isDown) {
            this.spaceShip.move(delta, Direction.Left);
        }
        else if (this.cursors.right.isDown) {
            this.spaceShip.move(delta, Direction.Right);
        }

        if (this.spaceKey.isDown) {
            this.fireBullet();
        }

        this.handleMeteors();
    }

    fireBullet() {
        if (this.time.now > this.bulletTime) {
            // Find the first unused (=unfired) bullet
            const bullet = this.bullets.get() as Bullet;
            if (bullet) {
                bullet.fire(this.spaceShip.x, this.spaceShip.y);
                this.bulletTime = this.time.now + 100;
            }
        }
    }

    handleMeteors() {
        // Check if it is time to launch a new meteor.
        if (this.time.now > this.meteorTime) {
            // Find first meteor that is currently not used
            const meteor = this.meteors.get() as Meteor;
            if (meteor) {
                meteor.fall();
                this.meteorTime = this.time.now + 500 + 1000 * Math.random();
            }
        }
    }

    gameOver() {
        if (!this.isGameOver) {
            ShooterScene.getScores();

            document.body.appendChild(document.createElement("br"));

            const input = document.createElement("input");
            input.setAttribute("elementId", "name");
            input.setAttribute("maxlength", "3");
            document.body.appendChild(input);

            const button = document.createElement('button');
            button.innerText = "Add Highscore";
            button.onclick = function () {
                if ((document.querySelector('input').value).length != 0) {
                    const table = document.querySelector('table');
                    table.parentNode.removeChild(table);

                    const req = request(
                        {
                            host: 'localhost',
                            port: '5001',
                            path: '/api/Players',
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        },
                        response => {
                            ShooterScene.getScores();
                        }
                    );

                    req.write(JSON.stringify({
                        name: document.querySelector('input').value,
                        points: ShooterScene.counter
                    }));

                    req.end();
                }
            };
            document.body.appendChild(button);
        }

        this.isGameOver = true;

        this.bullets.getChildren().forEach((b: Bullet) => b.kill());
        this.meteors.getChildren().forEach((m: Meteor) => m.kill());
        this.spaceShip.kill();

        // Display "game over" text
        const text = this.add.text(this.game.canvas.width / 2, this.game.canvas.height / 2, "Game Over :-(", 
            { font: "65px Arial", fill: "#ff0044", align: "center" });
        text.setOrigin(0.5, 0.5);
    }
    static getScores() {
        fetch('https://localhost:5001/api/Players').then(response => {
            response.json().then(players => {
                ShooterScene.generateTable(players);
            })
        })
    }

    static generateTable (players) {
        const tbl = document.createElement("table");
        const tblBody = document.createElement("tbody");
        let counter = 1;

        const row = document.createElement("tr");

        const cell3 = document.createElement("td");
        let cellText = document.createTextNode("Place");
        cell3.appendChild(cellText);
        row.appendChild(cell3);

        const cell = document.createElement("td");
        cellText = document.createTextNode("Name");
        cell.appendChild(cellText);
        row.appendChild(cell);

        const cell2 = document.createElement("td");
        cellText = document.createTextNode("Points");
        cell2.appendChild(cellText);
        row.appendChild(cell2);

        tblBody.appendChild(row);


        for (const player of players) {
            const row = document.createElement("tr");

            const cell3 = document.createElement("td");
            let cellText = document.createTextNode(counter + "");
            cell3.appendChild(cellText);
            row.appendChild(cell3);

            const cell = document.createElement("td");
            cellText = document.createTextNode(player.PName.value());
            cell.appendChild(cellText);
            row.appendChild(cell);

            const cell2 = document.createElement("td");
            cellText = document.createTextNode(player.Score.value());
            cell2.appendChild(cellText);
            row.appendChild(cell2);

            tblBody.appendChild(row);

            counter++;
        }
        tbl.appendChild(tblBody);

        document.body.appendChild(tbl);

        tbl.setAttribute("border", "2");
        tbl.setAttribute("width", "60%");
        tbl.setAttribute("id", "scores");
    }
}

const config = {
    type: CANVAS,
    width: 512,
    height: 512,
    scene: [ShooterScene],
    physics: { default: 'arcade' },
    audio: { noAudio: true }
};

new Game(config);
