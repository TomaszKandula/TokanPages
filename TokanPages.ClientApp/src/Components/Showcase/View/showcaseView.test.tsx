import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { ContentPageData } from "../../../Store/Defaults";
import { ShowcaseView } from "./showcaseView";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test component: showcaseView", () => {
    const testContent = {
        language: "en",
        caption: "Showcase",
        heading: "Most notable projects",
        text: "Let's look at some of...",
        image: "image.webp",
        action: {
            text: "Go to showcase",
            href: "/en/showcase",
        },
    };

    const pageData = ContentPageData;
    pageData.components.sectionShowcase = testContent;

    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce(pageData);
    });

    it("should render correctly '<ShowcaseView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <ShowcaseView />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
