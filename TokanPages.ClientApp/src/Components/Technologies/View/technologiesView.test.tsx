import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { render } from "enzyme";
import { TechnologiesView } from "./technologiesView";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn()
}));

describe("test component: technologiesView", () => {
    const testContent = {
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

    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce({
            isLoading: false,
            content: testContent
        });
    });

    it("should render correctly '<TechnologiesView />' when content is loaded.", () => {
        const html = render(<TechnologiesView />);
        expect(html).toMatchSnapshot();
    });
});
