import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { BrowserRouter as Router } from "react-router-dom";
import { render } from "enzyme";
import { HeaderView } from "./headerView";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test component: headerView", () => {
    const testContent = {
        language: "eng",
        photo: "",
        caption: "Welcome to my web page",
        description: "I do programming for a living...",
        action: {
            text: "Read the story",
            href: "/action-link",
        },
    };

    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce({
            isLoading: false,
            content: testContent,
        });
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
