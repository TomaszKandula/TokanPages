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
import { printSelfXssWarning } from "./xssWarning";
import { GetContentManifestDto } from "./Api/Models";
import App from "./app";

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href") as string;
const history = createBrowserHistory({ basename: baseUrl });
const store = ConfigureStore(history);

const ReactApp = (manifest: GetContentManifestDto): void => {
    ReactDOM.render(
        <Provider store={store}>
            <ConnectedRouter history={history}>
                <ThemeProvider theme={AppTheme}>
                    <CssBaseline />
                    <ErrorBoundary>
                        <App manifest={manifest} />
                    </ErrorBoundary>
                </ThemeProvider>
            </ConnectedRouter>
        </Provider>,
        document.getElementById("root")
    );
};

Loader.Initialize(ReactApp);
printSelfXssWarning();
export {};
