import * as React from "react";

import {Letter} from "./Letter";

export interface GameProperties
{
    endpoint: string;
}

export interface GameState {
    status: number;
    guesses?: string[];
    letters?: string[];
    id?: string;
    word?: string;
    remainingMissesCount?: number;
}

export class Game extends React.Component<GameProperties, GameState> {
    private headers: Headers;

    componentWillMount() {
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        this.setState({ status: 0 });
    }

    newGame() {
        fetch(`${this.props.endpoint}/new`, {
            method: 'POST',
            headers: this.headers
        }).then((response) => {
            return response.json();
        }).then(((response: GameState) => {
            this.setState(response);
            '1234567890abcdefghijklmnopqrstuvwxyz'.split('').map((el, i) => {
                var child:any = this.refs[el]
                child.reset();
            });
        }).bind(this));
    }

    guess(letter: string, cb:(correct:boolean) => void) {
        fetch(`${this.props.endpoint}/guess/${this.state.id}/${letter}`, {
            method: 'POST',
            headers: this.headers
        }).then((response) => {
            return response.json();
        }).then(((response: GameState) => {
            this.setState(response);
            let correct = response.letters.indexOf(letter) >= 0;
            cb(correct);
        }).bind(this));
    }

    render() {
        var game = this.state.status > 0 ?
            <div>
                <h1>{this.state.letters.join(' ') }</h1>
                <h1>You still have {this.state.remainingMissesCount} chances!</h1>
                <div id="letters" className="row">
                    <div className="col-md-5 col-sm-8 col-xs-8">
                        {'1234567890abcdefghijklmnopqrstuvwxyz'.split('').map((el, i) =>
                            <Letter ref={el} key={el} value={el} onGuess={this.guess.bind(this)} />
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