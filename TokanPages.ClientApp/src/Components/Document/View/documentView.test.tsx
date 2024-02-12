import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { ContentPolicyState, ContentTermsState, ContentStoryState } from "../../../Store/States";
import { TextItemDto } from "../../../Api/Models";
import { DocumentView } from "./documentView";

describe("test component: documentView", () => {
    it("should render correctly '<DocumentView />' when policy content is loaded.", () => {
        const textItem: TextItemDto = {
            id: "5b30be71-0e68-4be4-bd29-7f40fe130414",
            type: "html",
            value: "<p>Policy text</p>",
            prop: "",
            text: "",
        };

        const policyContent: ContentPolicyState = {
            isLoading: false,
            content: {
                language: "eng",
                items: [textItem],
            },
        };

        const tree = shallow(<DocumentView content={policyContent.content} isLoading={policyContent.isLoading} />);
        expect(tree).toMatchSnapshot();
    });

    it("should render correctly '<DocumentView />' when terms content is loaded.", () => {
        const textItem: TextItemDto = {
            id: "5b30be71-0e68-4be4-bd29-7f40fe130414",
            type: "html",
            value: "<p>Terms text</p>",
            prop: "",
            text: "",
        };

        const termsContent: ContentTermsState = {
            isLoading: false,
            content: {
                language: "eng",
                items: [textItem],
            },
        };

        const tree = shallow(<DocumentView content={termsContent.content} isLoading={termsContent.isLoading} />);
        expect(tree).toMatchSnapshot();
    });

    it("should render correctly '<DocumentView />' when story content is loaded.", () => {
        const textItem: TextItemDto = {
            id: "5b30be71-0e68-4be4-bd29-7f40fe130414",
            type: "html",
            value: "<p>Story text</p>",
            prop: "",
            text: "",
        };

        const storyContent: ContentStoryState = {
            isLoading: false,
            content: {
                language: "eng",
                items: [textItem],
            },
        };

        const tree = shallow(<DocumentView content={storyContent.content} isLoading={storyContent.isLoading} />);
        expect(tree).toMatchSnapshot();
    });
});
