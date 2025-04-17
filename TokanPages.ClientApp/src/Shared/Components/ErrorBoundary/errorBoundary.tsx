import React, { Component, ErrorInfo, ReactNode } from "react";
import { ErrorBoundaryView } from "./errorBoundaryView";
import { ExecuteActionRequest, ExecuteAsync, LOG_MESSAGE } from "../../../Api/Request";

interface Props {
    children?: ReactNode;
}

interface State {
    hasError: boolean;
}

interface LogMessage {
    eventDateTime: string;
    eventType: string;
    severity: string;
    message: string;
    stackTrace: string;
    pageUrl: string;
    browserName: string;
    browserVersion: string;
    userAgent: string;
}

export class ErrorBoundary extends Component<Props, State> {
    public state: State = {
        hasError: false,
    };

    public static getDerivedStateFromError(_: Error): State {
        return { hasError: true };
    }

    public async componentDidCatch(error: Error, errorInfo: ErrorInfo) {
        console.error("Uncaught error", error, errorInfo);

        const logMessage: LogMessage = {
            eventDateTime: new Date().toISOString(),
            eventType: "errorBoundary",
            severity: "error",
            message: JSON.stringify(error),
            stackTrace: JSON.stringify(errorInfo),
            pageUrl: window.location.href,
            browserName: "n/a",
            browserVersion: "n/a",
            userAgent: window.navigator.userAgent,
        };

        const request: ExecuteActionRequest = {
            url: LOG_MESSAGE,
            configuration: {
                method: "POST",
                body: logMessage,
            },
        };

        await ExecuteAsync(request);
    }

    public render() {
        if (this.state.hasError) {
            return <ErrorBoundaryView />;
        }

        return this.props.children;
    }
}
