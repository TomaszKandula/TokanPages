import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { render } from "@testing-library/react";
import { TechnologiesContentDto } from "../../../Api/Models";
import { ContentPageData } from "../../../Store/Defaults";
import { ApplicationDefault, ApplicationState } from "../../../Store/Configuration";
import { TechnologiesView } from "./technologiesView";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test component: technologiesView", () => {
    const testContent: TechnologiesContentDto = {
        language: "eng",
        caption: "Technologies",
        header: "I work primarily with",
        title1: "Languages",
        text1: "Today in my daily job, I use...",
        title2: "Frameworks/Libraries",
        text2: "Back-End: NET Framework 4.5 (for one project)...",
        title3: "OR/M",
        text3: "I have started with Entity Framework...",
        title4: "Cloud Services",
        text4: "I have experience with...",
    };

    let state: ApplicationState = ApplicationDefault;
    state.contentPageData = ContentPageData;
    state.contentPageData.components.sectionTechnologies = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    beforeEach(() => {
        useSelectorMock.mockImplementation(callback => callback(state));
    });

    it("should render correctly '<TechnologiesView />' when content is loaded.", () => {
        const html = render(<TechnologiesView />);
        expect(useSelectorMock).toBeCalledTimes(1);
        expect(html).toMatchSnapshot();
    });
});
