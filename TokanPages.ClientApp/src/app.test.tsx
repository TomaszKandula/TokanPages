/**
 * @jest-environment jsdom
 */
import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import { GetContentManifestDto } from "./Api/Models";
import App from "./app";

window.scrollTo = jest.fn();
jest.mock("@unhead/react", () => ({
    useHead: jest.fn(),
}));

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
        default: "x-default",
        flagImageDir: "flags",
        flagImageType: "webp",
        languages: [
            {
                id: "en",
                iso: "en-GB",
                isDefault: true,
                name: "English",
            },
            {
                id: "pl",
                iso: "pl-PL",
                isDefault: false,
                name: "Polski",
            },
        ],
        warnings: [],
        pages: [
            {
                language: "en",
                pages: [
                    {
                        title: "software developer",
                        page: "MainPage",
                        description: "software development",
                    },
                ],
            },
        ],
        meta: [
            {
                language: "en",
                facebook: {
                    title: "",
                    description: "",
                    imageAlt: "",
                },
                twitter: {
                    title: "",
                    description: "",
                },
            },
        ],
        errorBoundary: [
            {
                language: "en",
                title: "Critical Error",
                subtitle: "Something went wrong...",
                text: "Contact the site's administrator or support for assistance.",
                linkHref: "mailto:admin@tomkandula.com",
                linkText: "IT support",
                footer: "tomkandula.com",
            },
        ],
    };

    const root = document.createElement("div");
    root.setAttribute("id", "root");
    document.body.appendChild(root);

    ReactDOM.render(
        <Provider store={store}>
            <MemoryRouter>
                <App manifest={manifest} />
            </MemoryRouter>
        </Provider>,
        root
    );
});
