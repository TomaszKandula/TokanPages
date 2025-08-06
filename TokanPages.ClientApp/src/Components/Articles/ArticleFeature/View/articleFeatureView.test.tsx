import "../../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { BrowserRouter as Router } from "react-router-dom";
import { render } from "@testing-library/react";
import { ArticleFeaturesContentDto } from "../../../../Api/Models";
import { ContentPageData } from "../../../../Store/Defaults";
import { ApplicationDefault, ApplicationState } from "../../../../Store/Configuration";
import { ArticleFeatureView } from "./articleFeatureView";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test articles group component: ArticleFeatureView", () => {
    const testContent: ArticleFeaturesContentDto = {
        language: "eng",
        caption: "Articles",
        title: ".NET Core, Azure, databases and others.",
        description: "I write regularly...",
        text: "Let's dive into Microsoft technology...",
        action: {
            text: "View list of articles",
            href: "/action-link",
        },
        image1: "image1.jpg",
        image2: "image2.jpg",
        image3: "image3.jpg",
        image4: "image4.jpg",
    };

    let state: ApplicationState = ApplicationDefault;
    state.contentPageData = ContentPageData;
    state.contentPageData.components.sectionArticle = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    beforeEach(() => {
        useSelectorMock.mockImplementation(callback => callback(state));
    });

    it("should render correctly '<ArticleFeatureView />' when content is loaded.", () => {
        const html = render(
            <Router>
                <ArticleFeatureView />
            </Router>
        );

        expect(useSelectorMock).toBeCalledTimes(3);
        expect(html).toMatchSnapshot();
    });
});
