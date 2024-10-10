import "../../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { BrowserRouter as Router } from "react-router-dom";
import { render } from "@testing-library/react";
import { ArticleFeatureView } from "./articleFeatureView";
import { ContentPageData } from "../../../../Store/Defaults";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test articles group component: ArticleFeatureView", () => {
    const testContent = {
        language: "eng",
        title: "Articles",
        description: "I write regularly...",
        text1: ".NET Core, Azure, databases and others.",
        text2: "Let's dive into Microsoft technology...",
        action: {
            text: "View list of articles",
            href: "/action-link",
        },
        image1: "image1.jpg",
        image2: "image2.jpg",
        image3: "image3.jpg",
        image4: "image4.jpg",
    };

    const pageData = ContentPageData;
    pageData.components.articleFeatures = testContent;

    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce(pageData);
    });

    it("should render correctly '<ArticleFeatureView />' when content is loaded.", () => {
        const html = render(
            <Router>
                <ArticleFeatureView />
            </Router>
        );
        expect(html).toMatchSnapshot();
    });
});
