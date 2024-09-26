import "../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { render } from "@testing-library/react";
import { Testimonials } from "./testimonials";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test component: testimonials", () => {
    const testContent = {
        language: "eng",
        caption: "Testimonials",
        subtitle: "You can read few commendations...",
        photo1: "joanna.webp",
        name1: "Joanna",
        occupation1: "Senior Digital Tax Specialist",
        text1: "Joe is very motivated...",
        photo2: "adama.webp",
        name2: "Adama",
        occupation2: "Full-stack Developer chez DFDS POLSKA",
        text2: "Joe have done very good work...",
        photo3: "scott.webp",
        name3: "Scott",
        occupation3: "BPO",
        text3: "Joe is one of those rare talents...",
    };

    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce({
            isLoading: false,
            content: testContent,
        });
    });

    it("should render correctly '<Testimonials />' when content is loaded.", () => {
        const html = render(<Testimonials />);
        expect(html).toMatchSnapshot();
    });
});
