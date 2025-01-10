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
        return { hasError: true };
    }

    public componentDidCatch(error: Error, errorInfo: ErrorInfo) {
        console.error("Uncaught error:", error, errorInfo);
    }

    public render() {
        if (this.state.hasError) {
            return (
                <div id="error">
                    <div className="error">
                        <h1>Critical Error</h1>
                        <h2>Something went wrong...</h2>
                        <div className="error-text">
                            <p>Contact the site's administrator or support for assistance.</p>
                            <a href="mailto:admin@tomkandula.com">IT support</a>
                            <hr />
                            <p>tomkandula.com</p>
                        </div>
                    </div>
                </div>
            );
        }

        return this.props.children;
    }
}
