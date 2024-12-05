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
        photo: {
            w360: "image1-360w.webp",
            w720: "image1-720w.webp",
            w1440: "image1-1440w.webp",
            w2880: "image1-2880w.webp",
        },
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
