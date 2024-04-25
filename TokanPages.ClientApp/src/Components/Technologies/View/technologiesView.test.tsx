import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { shallow } from "enzyme";
import { ApplicationDefault } from "../../../Store/Configuration";
import { TechnologiesView } from "./technologiesView";

describe("test component: featuresView", () => {
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
    
    let data = ApplicationDefault;
    data.contentFeatures.isLoading = false;
    data.contentFeatures.content = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const wrapper = shallow(
        <div>
            <TechnologiesView />
        </div>
    );

    beforeEach(() => {
        useSelectorMock.mockClear();
        wrapper.find("TechnologiesView").dive();
    });

    it("should render correctly '<FeaturesView />' when content is loaded.", () => {
        useSelectorMock.mockReturnValue(data);
        expect(wrapper).toMatchSnapshot();
    });
});
