import * as React from "react";

export interface LetterProperties {
    value: string;
    onGuess: (letter: string, cb: (correct: boolean) => void) => void;
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
        this.props.onGuess(this.props.value, (correct) => {
            this.setState({ enabled: false, correct });
        });
    }

    reset() {
        this.setState({ enabled: true });
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