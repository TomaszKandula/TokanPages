import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import TestimonialsView from "../testimonialsView";

describe("Test component: testimonialsView.", () => 
{
    it("Renders correctly '<TestimonialsView />' when content is loaded.", () => 
    {
        const tree = shallow(<TestimonialsView isLoading={false} content=
        {{
            caption: "Testimonials",
            subtitle: "You can read few commendations...",
            photo1: "joanna.jpg",
            name1: "Joanna",
            occupation1: "Senior Digital Tax Specialist",
            text1: "Joe is very motivated...",
            photo2: "adama.jpg",
            name2: "Adama",
            occupation2: "Full-stack Developer chez DFDS POLSKA",
            text2: "Joe have done very good work...",
            photo3: "scott.jpg",
            name3: "Scott",
            occupation3: "BPO",
            text3: "Joe is one of those rare talents..."
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
