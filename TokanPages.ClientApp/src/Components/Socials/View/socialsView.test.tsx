import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { render } from "@testing-library/react";
import { SocialsContentDto } from "../../../Api/Models";
import { ContentPageData } from "../../../Store/Defaults";
import { ApplicationDefault, ApplicationState } from "../../../Store/Configuration";
import { SocialsView } from "./socialsView";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test component: socialsView", () => {
    const testContent: SocialsContentDto = {
        language: "eng",
        caption: "Social Media",
        social1: {
            images: {
                header: "linkedin_header.webp",
                avatar: "linkedin_user.webp",
                icon: "linkedin",
            },
            textTitle: "Somebody",
            textSubtitle: "Software Engineer...",
            textComment: "Warsaw Metropolitan Area",
            action: {
                text: "Visit",
                href: "https://google.com",
            },
        },
        social2: {
            images: {
                header: "github_header.webp",
                avatar: "github_user.webp",
                icon: "github",
            },
            textTitle: "Somebody",
            textSubtitle: "JavaScript/TypeScript...",
            textComment: "GitHub Repository",
            action: {
                text: "Visit",
                href: "https://google.com",
            },
        },
        social3: {
            images: {
                header: "instagram_header.webp",
                avatar: "instagram_user.webp",
                icon: "instagram",
            },
            textTitle: "TomKandula",
            textSubtitle: "Photographer 📸 ...",
            textComment: "Personal photo blog",
            action: {
                text: "Visit",
                href: "https://google.com",
            },
        },
    };

    let state: ApplicationState = ApplicationDefault;
    state.contentPageData = ContentPageData;
    state.contentPageData.components.sectionSocials = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    beforeEach(() => {
        useSelectorMock.mockImplementation(callback => callback(state));
    });

    it("should render correctly '<SocialsView />' when content is loaded.", () => {
        const html = render(<SocialsView />);
        expect(useSelectorMock).toBeCalledTimes(1);
        expect(html).toMatchSnapshot();
    });
});
