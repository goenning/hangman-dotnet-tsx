import * as React from "react";

export interface LetterProperties {
    value: string;
    onGuess: (letter: string) => void;
}

export interface LetterState {
    enabled: boolean;
    correct?: boolean;
}

export class Letter extends React.Component<LetterProperties, LetterState> {
    componentWillMount() {
        this.setState({ enabled: true });
    }

    guess() {
        this.setState({ enabled: false, correct: true });
        this.props.onGuess(this.props.value);
    }

    getCssClass() {
        if (this.state.enabled)
            return "btn btn-default";

        return (this.state.correct) ? "btn btn-success" : "btn btn-danger";
    }

    render() {
        return <div>
            <button type="button" className={this.getCssClass()} disabled={!this.state.enabled} onClick={this.guess.bind(this) }>
                {this.props.value.toUpperCase()}
            </button>
        </div>;
    }
}