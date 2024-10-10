import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { BrowserRouter as Router } from "react-router-dom";
import { render } from "@testing-library/react";
import { HeaderView } from "./headerView";
import { ContentPageData } from "../../../Store/Defaults";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test component: headerView", () => {
    const testContent = {
        language: "eng",
        photo: "ester-exposito.webp",
        caption: "Welcome to my web page",
        description: "I do programming for a living...",
        subtitle: "For years...",
        action: {
            text: "Read the story",
            href: "/action-link",
        },
        resume: {
            text: "Get the CV",
            href: "/download-link",
        },    
    };

    const pageData = ContentPageData;
    pageData.components.header = testContent;

    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce(pageData);
    });

    it("should render correctly '<HeaderView />' when content is loaded.", () => {
        const html = render(
            <Router>
                <HeaderView />
            </Router>
        );
        expect(html).toMatchSnapshot();
    });
});
