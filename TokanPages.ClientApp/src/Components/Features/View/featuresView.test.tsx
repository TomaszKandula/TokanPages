import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { shallow } from "enzyme";
import { ApplicationDefault } from "../../../Store/Configuration";
import { FeaturesView } from "./featuresView";

describe("test articles group component: ArticleFeaturesView", () => {
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
    
    let data = ApplicationDefault;
    data.contentArticleFeatures.isLoading = false;
    data.contentArticleFeatures.content = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const wrapper = shallow(
        <div>
            <FeaturesView />
        </div>
    );

    beforeEach(() => {
        useSelectorMock.mockClear();
        wrapper.find("FeaturesView").dive();
    });
    
    it("should render correctly '<ArticleFeaturesView />' when content is loaded.", () => {
        useSelectorMock.mockReturnValue(data);
        expect(wrapper).toMatchSnapshot();
    });
});
