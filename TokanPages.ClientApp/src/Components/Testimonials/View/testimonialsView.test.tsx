import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { shallow } from "enzyme";
import { ApplicationDefault } from "../../../Store/Configuration";
import { TestimonialsView } from "./testimonialsView";

describe("test component: testimonialsView", () => {
    const testContent = {
        language: "eng",
        caption: "Testimonials",
        subtitle: "You can read few commendations...",
        photo1: "",
        name1: "Joanna",
        occupation1: "Senior Digital Tax Specialist",
        text1: "Joe is very motivated...",
        photo2: "",
        name2: "Adama",
        occupation2: "Full-stack Developer chez DFDS POLSKA",
        text2: "Joe have done very good work...",
        photo3: "",
        name3: "Scott",
        occupation3: "BPO",
        text3: "Joe is one of those rare talents...",
    };
    
    let data = ApplicationDefault;
    data.contentTestimonials.isLoading = false;
    data.contentTestimonials.content = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const wrapper = shallow(
        <div>
            <TestimonialsView />
        </div>
    );

    beforeEach(() => {
        useSelectorMock.mockClear();
        wrapper.find("TestimonialsView").dive();
    });

    it("should render correctly '<TestimonialsView />' when content is loaded.", () => {
        useSelectorMock.mockReturnValue(data);
        expect(wrapper).toMatchSnapshot();
    });
});
