import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { render } from "@testing-library/react";
import { FeaturedContentDto } from "../../../Api/Models";
import { ContentPageData } from "../../../Store/Defaults";
import { ApplicationDefault, ApplicationState } from "../../../Store/Configuration";
import { FeaturedView } from "./featuredView";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test component: featuredView", () => {
    const testContent: FeaturedContentDto = {
        language: "eng",
        caption: "Featured",
        text: "I picked three articles that I wrote...",
        title1: "Memory Management",
        subtitle1: "The basics you should know, when writing an application...",
        link1: "https://.../net-memory-management-64389b01f642",
        image1: "article0.jpg",
        title2: "Stored Procedures",
        subtitle2: "I explain why I do not need them that much...",
        link2: "https://.../i-said-goodbye-to-stored-procedures-539d56350486",
        image2: "article1.jpg",
        title3: "SQL Injection",
        subtitle3: "This article will explore the issue in greater detail...",
        link3: "https://.../sql-injection-1bde8bb76ebc",
        image3: "article2.jpg",
    };

    let state: ApplicationState = ApplicationDefault;
    state.contentPageData = ContentPageData;
    state.contentPageData.components.sectionFeatured = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    beforeEach(() => {
        useSelectorMock.mockImplementation((callback) => callback(state));
    });

    it("should render correctly '<FeaturedView />' when content is loaded.", () => {
        const html = render(<FeaturedView />);
        expect(useSelectorMock).toBeCalledTimes(1);
        expect(html).toMatchSnapshot();
    });
});
