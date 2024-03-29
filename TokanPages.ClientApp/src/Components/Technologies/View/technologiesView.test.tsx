import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { TechnologiesView } from "./technologiesView";

describe("test component: featuresView", () => {
    it("should render correctly '<FeaturesView />' when content is loaded.", () => {
        const tree = shallow(
            <TechnologiesView
                isLoading={false}
                content={{
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
                }}
            />
        );
        expect(tree).toMatchSnapshot();
    });
});
