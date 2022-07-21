import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { StaticContentView } from "./staticContentView";
import { ITextItem, ITextObject } from "../../../Shared/Components/ContentRender/Models/textModel";

describe("Test component: staticContentView.", () => 
{
    it("Renders correctly '<StaticContentView />' when content is loaded.", () => 
    {
        const textItem: ITextItem = 
        {
            id: "5b30be71-0e68-4be4-bd29-7f40fe130414",
            type: "html",
            value: "<p>One line text</p>",
            prop: "",
            text: ""
        };

        const textObject: ITextObject = 
        {
            items: [textItem]
        };

        const tree = shallow(<StaticContentView bind=
        {{
            isLoading: false,
            data: textObject
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
