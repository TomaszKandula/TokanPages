import * as React from "react";
import * as ReactDOM from "react-dom";
import * as Loader from "loader";
import { Provider } from "react-redux";
import { ConnectedRouter } from "connected-react-router";
import { createBrowserHistory } from "history";
import { ThemeProvider } from "@material-ui/core";
import { AppTheme } from "./Theme";
import { ConfigureStore } from "./Store/Configuration";
import { ErrorBoundary } from "./Shared/Components";
import { IsPreRendered, TryGetStateSnapshot } from "./Shared/Services/SpaCaching";
import { printSelfXssWarning } from "./xssWarning";
import { GetContentManifestDto } from "./Api/Models";
import "./Theme/styles.css";
import App from "./app";

const root = document.getElementById("root");
const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href") as string;
const history = createBrowserHistory({ basename: baseUrl });
const initialState = TryGetStateSnapshot();
const store = ConfigureStore(history, initialState);

const ReactApp = (manifest: GetContentManifestDto | undefined): void => {
    const AppWrapper = () => {
        return (
            <Provider store={store}>
                <ConnectedRouter history={history}>
                    <ThemeProvider theme={AppTheme}>
                        <ErrorBoundary>
                            <App manifest={manifest} />
                        </ErrorBoundary>
                    </ThemeProvider>
                </ConnectedRouter>
            </Provider>
        );
    };

    IsPreRendered() ? ReactDOM.hydrate(<AppWrapper />, root) : ReactDOM.render(<AppWrapper />, root);
};

Loader.Initialize(ReactApp);
printSelfXssWarning();
export {};
