import React, { Component, ErrorInfo } from "react";
import { useApiAction } from "../../../Shared/Hooks";
import { Props, State } from "./Types";
import { ErrorBoundaryView } from "./errorBoundaryView";

export class ErrorBoundary extends Component<Props, State> {
    public state: State = {
        hasError: false,
    };

    public static getDerivedStateFromError(_: Error): State {
        return { hasError: true };
    }

    public componentDidCatch(error: Error, errorInfo: ErrorInfo) {
        const actions = useApiAction();
        console.error("Uncaught error", error, errorInfo);
        actions.logAction(error, errorInfo, "ErrorBoundary", "error");
    }

    public render() {
        if (this.state.hasError) {
            return <ErrorBoundaryView />;
        }

        return this.props.children;
    }
}
