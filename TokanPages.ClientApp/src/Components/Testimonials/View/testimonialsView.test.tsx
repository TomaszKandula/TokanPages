import "../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { TestimonialsView } from "./testimonialsView";

describe("test component: testimonialsView", () => {
    const testContent = {
        language: "eng",
        caption: "Testimonials",
        subtitle: "You can read few commendations...",
        photo1: "joanna.webp",
        name1: "Joanna",
        link1: "https://www.linkedin.com/in/joanna",
        occupation1: "Senior Digital Tax Specialist",
        text1: "Joe is very motivated...",
        photo2: "adama.webp",
        name2: "Adama",
        link2: "https://www.linkedin.com/in/adama",
        occupation2: "Full-stack Developer chez DFDS POLSKA",
        text2: "Joe have done very good work...",
        photo3: "scott.webp",
        name3: "Scott",
        link3: "https://www.linkedin.com/in/scott",
        occupation3: "BPO",
        text3: "Joe is one of those rare talents...",
    };

    it("should render correctly '<TestimonialsView />' when content is loaded.", () => {
        const html = render(
            <TestimonialsView
                isLoading={false}
                caption={testContent.caption}
                subtitle={testContent.subtitle}
                Testimonial1={{
                    photo: testContent?.photo1,
                    name: testContent?.name1,
                    link: testContent?.link1,
                    occupation: testContent?.occupation1,
                    text: testContent?.text1,
                }}
                Testimonial2={{
                    photo: testContent?.photo2,
                    name: testContent?.name2,
                    link: testContent?.link2,
                    occupation: testContent?.occupation2,
                    text: testContent?.text2,
                }}
                Testimonial3={{
                    photo: testContent?.photo3,
                    name: testContent?.name3,
                    link: testContent?.link3,
                    occupation: testContent?.occupation3,
                    text: testContent?.text3,
                }}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
