import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";

interface ErrorBoundaryProps {
    children: React.ReactNode;
}

interface ErrorPromptProps {
    title: string;
    subtitle: string;
    text: string;
    link: {
        href: string;
        text: string;
    };
    footer: string;
}

const ErrorPrompt = (props: ErrorPromptProps): React.ReactElement => {
    return(
        <div id="error">
            <div className="error">
                <h1>{props.title}</h1>
                <h2>{props.subtitle}</h2>
                <div className="error-text">
                    <p>{props.text}</p>
                    <a href={props.link.href}>{props.link.text}</a>
                    <hr />
                    <p>{props.footer}</p>
                </div>
            </div>
        </div>
    );
}

export const ErrorBoundary = (props: ErrorBoundaryProps): React.ReactElement => {
    const [hasError, setHasError] = React.useState(false);
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        const errorHandler = (event: ErrorEvent) => {
            setHasError(true);
            console.error("ErrorBoundary caught an error", event.error);
        };

        const rejectionHandler = (event: PromiseRejectionEvent) => {
            setHasError(true);
            console.error("ErrorBoundary caught an error", event.reason);
        }

        window.addEventListener("error", errorHandler);
        window.addEventListener("unhandledrejection", rejectionHandler);

        return () => {
            window.removeEventListener("error", errorHandler);
            window.removeEventListener("unhandledrejection", rejectionHandler);
        };
    }, []);

    console.log(language);

    if (hasError) {
        return (
            <ErrorPrompt
                title=""
                subtitle=""
                text=""
                link={{ href: "", text: "" }}
                footer=""
            />
        );
    }

    return <>{props.children}</>;
}
