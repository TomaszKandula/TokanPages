import React, { Component, ErrorInfo, ReactNode } from "react";

interface Props {
    children?: ReactNode;
}

interface State {
    hasError: boolean;
}

export class ErrorBoundary extends Component<Props, State> {
    public state: State = {
        hasError: false,
    };

    public static getDerivedStateFromError(_: Error): State {
        // Update state so the next render will show the fallback UI.
        return { hasError: true };
    }

    public componentDidCatch(error: Error, errorInfo: ErrorInfo) {
        console.error("Uncaught error:", error, errorInfo);
    }

    public render() {
        if (this.state.hasError) {
            return (
                <div>
                    <h2>Something went wrong...</h2>
                    <p>
                        Please try again, if problem persists contact:{" "}
                        <a href="mailto:admin@tomkandula.com">IT support</a>.
                    </p>
                </div>
            );
        }

        return this.props.children;
    }
}
