import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { BrowserRouter as Router } from "react-router-dom";
import { render } from "@testing-library/react";
import { HeaderContentDto } from "../../../Api/Models";
import { ContentPageData } from "../../../Store/Defaults";
import { ApplicationDefault, ApplicationState } from "../../../Store/Configuration";
import { HeaderView } from "./headerView";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test component: headerView", () => {
    const testContent: HeaderContentDto = {
        language: "eng",
        photo: {
            w360: "image1-360w.webp",
            w720: "image1-720w.webp",
            w1440: "image1-1440w.webp",
            w2880: "image1-2880w.webp",
        },
        caption: "Welcome to my web page",
        description: "I do programming for a living...",
        hint: "Let me know...",
        subtitle: "For years...",
        primaryButton: {
            text: "Read the story",
            href: "/en/action-link",
        },
        secondaryButton: {
            text: "Get the CV",
            href: "/en/download-link",
        },
        tertiaryButton: {
            text: "Showcase",
            href: "/en/showcase",
        },
    };

    let state: ApplicationState = ApplicationDefault;
    state.contentPageData = ContentPageData;
    state.contentPageData.components.layoutHeader = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    beforeEach(() => {
        useSelectorMock.mockImplementation(callback => callback(state));
    });

    it("should render correctly '<HeaderView />' when content is loaded.", () => {
        const html = render(
            <Router>
                <HeaderView />
            </Router>
        );

        expect(useSelectorMock).toBeCalledTimes(2);
        expect(html).toMatchSnapshot();
    });
});
