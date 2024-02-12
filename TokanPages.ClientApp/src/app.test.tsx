/**
 * @jest-environment jsdom
 */
import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import { GetContentManifestDto } from "./Api/Models";
import App from "./app";

it("renders without crashing", () => {
    const storeFake = (state: any) => ({
        default: () => {
            // Leave blank for tests
        },
        subscribe: () => {
            // Leave blank for tests
        },
        dispatch: () => {
            // Leave blank for tests
        },
        getState: () => ({ ...state }),
    });

    const store = storeFake({}) as any;
    const manifest: GetContentManifestDto = {
        version: "0.1",
        created: "2022-09-09",
        updated: "2022-09-09",
        languages: [
            {
                id: "eng",
                isDefault: true,
                name: "English",
            },
            {
                id: "pol",
                isDefault: false,
                name: "Polski",
            },
        ],
    };

    ReactDOM.render(
        <Provider store={store}>
            <MemoryRouter>
                <App manifest={manifest} />
            </MemoryRouter>
        </Provider>,
        document.createElement("div")
    );
});
