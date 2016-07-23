import * as React from "react";

import {Letter} from "./Letter";

export interface GameProperties
{
}

export interface GameState {
    remainingMisses?: number;
    letters?: string[];
    status: number;
}

export class Game extends React.Component<GameProperties, GameState> {
    componentWillMount() {
        this.setState({ status: 0 });
    }

    newGame() {
        this.setState({ status: 1, remainingMisses: 3, letters: ['_', '_', '_'] });
    }

    guess(letter: string) {
        console.log(letter);
    }

    render() {
        var game = this.state.status > 0 ?
            <div>
                <h1>{this.state.letters.join(' ') }</h1>
                <h1>You still have {this.state.remainingMisses} chances!</h1>
                <div id="letters" className="row">
                    <div className="col-md-5 col-sm-8 col-xs-8">
                        {'1234567890abcdefghijklmnopqrstuvwxyz'.split('').map((el, i) =>
                            <Letter key={el} value={el} onGuess={this.guess.bind(this, el)} />
                        )}
                    </div>
                </div>
            </div> : null;

        return <div>
            <button type="button" className="btn btn-primary" onClick={this.newGame.bind(this)}>Start new game!</button>
            { game }
        </div>;
    }
}