import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { FeatureShowcaseContentDto } from "../../../Api/Models";
import { ContentPageData } from "../../../Store/Defaults";
import { ApplicationDefault, ApplicationState } from "../../../Store/Configuration";
import { ShowcaseView } from "./showcaseView";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test component: showcaseView", () => {
    const testContent: FeatureShowcaseContentDto = {
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

    let state: ApplicationState = ApplicationDefault;
    state.contentPageData = ContentPageData;
    state.contentPageData.components.sectionShowcase = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    beforeEach(() => {
        useSelectorMock.mockImplementation(callback => callback(state));
    });

    it("should render correctly '<ShowcaseView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <ShowcaseView />
            </BrowserRouter>
        );

        expect(useSelectorMock).toBeCalledTimes(2);
        expect(html).toMatchSnapshot();
    });
});
