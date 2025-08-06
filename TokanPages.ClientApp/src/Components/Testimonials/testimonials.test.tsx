import "../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { render } from "@testing-library/react";
import { TestimonialsContentDto } from "../../Api/Models";
import { ContentPageData } from "../../Store/Defaults";
import { ApplicationDefault, ApplicationState } from "../../Store/Configuration";
import { Testimonials } from "./testimonials";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test component: testimonials", () => {
    const testContent: TestimonialsContentDto = {
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

    let state: ApplicationState = ApplicationDefault;
    state.contentPageData = ContentPageData;
    state.contentPageData.components.sectionTestimonials = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    beforeEach(() => {
        useSelectorMock.mockImplementation(callback => callback(state));
    });

    it("should render correctly '<Testimonials />' when content is loaded.", () => {
        const html = render(<Testimonials />);
        expect(useSelectorMock).toBeCalledTimes(3);
        expect(html).toMatchSnapshot();
    });
});
