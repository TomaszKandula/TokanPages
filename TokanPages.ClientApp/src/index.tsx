import * as React from "react";
import * as ReactDOM from "react-dom";
import * as Loader from "loader";
import { Provider } from "react-redux";
import { ConnectedRouter } from "connected-react-router";
import { createBrowserHistory } from "history";
import { ThemeProvider } from "@material-ui/core";
import { AppTheme } from "./Theme";
import CssBaseline from "@material-ui/core/CssBaseline";
import { ConfigureStore } from "./Store/Configuration";
import { ErrorBoundary } from "./Shared/Components";
import { GetSnapshotState } from "./Shared/Services/SpaCaching";
import { printSelfXssWarning } from "./xssWarning";
import { GetContentManifestDto } from "./Api/Models";
import App from "./app";

const root = document.getElementById("root");
const isPreRendered = root?.hasChildNodes();
const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href") as string;
const history = createBrowserHistory({ basename: baseUrl });

const ReactApp = (manifest: GetContentManifestDto): void => {
    const initialState = isPreRendered ? GetSnapshotState() : undefined;
    const store = ConfigureStore(history, initialState);
    const AppWrapper = () => { 
        return (
            <Provider store={store}>
                <ConnectedRouter history={history}>
                    <ThemeProvider theme={AppTheme}>
                        <CssBaseline />
                        <ErrorBoundary>
                            <App manifest={manifest} />
                        </ErrorBoundary>
                    </ThemeProvider>
                </ConnectedRouter>
            </Provider>
        )
    };

    isPreRendered ? ReactDOM.hydrate(<AppWrapper />, root) : ReactDOM.render(<AppWrapper />, root);
};

Loader.Initialize(ReactApp);
printSelfXssWarning();
export {};
