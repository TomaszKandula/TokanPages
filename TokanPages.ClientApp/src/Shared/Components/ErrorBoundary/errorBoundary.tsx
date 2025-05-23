import React, { Component, ErrorInfo, ReactNode } from "react";
import { ErrorBoundaryView } from "./errorBoundaryView";
import { useApiAction } from "../../../Shared/Hooks";

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
