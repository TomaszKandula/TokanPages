import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { IContentPolicy, IContentTerms, IContentStory } from "../../../Store/States";
import { ITextItem } from "../../../Api/Models";
import { DocumentView } from "./documentView";

describe("Test component: documentView.", () => 
{
    it("Renders correctly '<DocumentView />' when policy content is loaded.", () => 
    {
        const textItem: ITextItem = 
        {
            id: "5b30be71-0e68-4be4-bd29-7f40fe130414",
            type: "html",
            value: "<p>Policy text</p>",
            prop: "",
            text: ""
        };

        const policyContent: IContentPolicy = 
        {
            isLoading: false,
            content: 
            {
                language: "eng",
                items: [textItem]
            }
        };

        const tree = shallow(<DocumentView 
            content={policyContent.content} 
            isLoading={policyContent.isLoading} 
        />);
        expect(tree).toMatchSnapshot();
    });

    it("Renders correctly '<DocumentView />' when terms content is loaded.", () => 
    {
        const textItem: ITextItem = 
        {
            id: "5b30be71-0e68-4be4-bd29-7f40fe130414",
            type: "html",
            value: "<p>Terms text</p>",
            prop: "",
            text: ""
        };

        const termsContent: IContentTerms = 
        {
            isLoading: false,
            content: 
            {
                language: "eng",
                items: [textItem]
            }
        };

        const tree = shallow(<DocumentView 
            content={termsContent.content} 
            isLoading={termsContent.isLoading} 
        />);
        expect(tree).toMatchSnapshot();
    });

    it("Renders correctly '<DocumentView />' when story content is loaded.", () => 
    {
        const textItem: ITextItem = 
        {
            id: "5b30be71-0e68-4be4-bd29-7f40fe130414",
            type: "html",
            value: "<p>Story text</p>",
            prop: "",
            text: ""
        };

        const storyContent: IContentStory = 
        {
            isLoading: false,
            content: 
            {
                language: "eng",
                items: [textItem]
            }
        };

        const tree = shallow(<DocumentView 
            content={storyContent.content} 
            isLoading={storyContent.isLoading} 
        />);
        expect(tree).toMatchSnapshot();
    });
});
