import React, { Component, ErrorInfo, ReactNode } from "react";
import { ErrorBoundaryView } from "./errorBoundaryView";
import { ExecuteActionRequest, ExecuteAsync, LOG_MESSAGE } from "../../../Api";
import { UAParser } from "ua-parser-js";
import { LogMessageDto } from "../../../Api/Models";

interface Props {
    children?: ReactNode;
}

interface State {
    hasError: boolean;
}

const LogError = async (error: Error, errorInfo: ErrorInfo): Promise<void> => {
    const ua = UAParser(window.navigator.userAgent);

    const logMessage: LogMessageDto = {
        eventDateTime: new Date().toISOString(),
        eventType: "errorBoundary",
        severity: "error",
        message: JSON.stringify(error),
        stackTrace: JSON.stringify(errorInfo),
        pageUrl: window.location.href,
        userAgent: window.navigator.userAgent,
        clientData: {
            browser: {
                major: ua.browser.major,
                name: ua.browser.name,
                type: ua.browser.type,
                version: ua.browser.version,
            },
            cpu: {
                architecture: ua.cpu.architecture,
            },
            device: {
                model: ua.device.model,
                type: ua.device.type,
                vendor: ua.device.vendor,
            },
            engine: {
                name: ua.engine.name,
                version: ua.engine.version,
            },
            os: {
                name: ua.os.name,
                version: ua.os.version,
            },
        },
    };

    const request: ExecuteActionRequest = {
        url: LOG_MESSAGE,
        configuration: {
            method: "POST",
            hasJsonResponse: true,
            body: logMessage,
        },
    };

    await ExecuteAsync(request);
}

export class ErrorBoundary extends Component<Props, State> {
    public state: State = {
        hasError: false,
    };

    public static getDerivedStateFromError(_: Error): State {
        return { hasError: true };
    }

    public componentDidCatch(error: Error, errorInfo: ErrorInfo) {
        console.error("Uncaught error", error, errorInfo);
        LogError(error, errorInfo);
    }

    public render() {
        if (this.state.hasError) {
            return <ErrorBoundaryView />;
        }

        return this.props.children;
    }
}
