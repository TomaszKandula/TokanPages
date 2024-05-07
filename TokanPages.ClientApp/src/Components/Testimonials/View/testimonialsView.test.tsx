import "../../../setupTests";
import React from "react";
import { render } from "enzyme";
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

    it("should render correctly '<TestimonialsView />' when content is loaded.", () => {
        const html = render(<TestimonialsView 
            isLoading={false}
            hasTestimonialOne={true}
            hasTestimonialTwo={true}
            hasTestimonialThree={true}
            buttonTestimonialOne={jest.fn()}
            buttonTestimonialTwo={jest.fn()}
            buttonTestimonialThree={jest.fn()}
            caption={testContent.caption}
            subtitle={testContent.subtitle}
            photo1={testContent.photo1}
            name1={testContent.name1}
            occupation1={testContent.occupation1}
            text1={testContent.text1}
            photo2={testContent.photo2}
            name2={testContent.name2}
            occupation2={testContent.occupation2}
            text2={testContent.text2}
            photo3={testContent.name3}
            name3={testContent.name3}
            occupation3={testContent.occupation3}
            text3={testContent.text3}
        />);

        expect(html).toMatchSnapshot();
    });
});
