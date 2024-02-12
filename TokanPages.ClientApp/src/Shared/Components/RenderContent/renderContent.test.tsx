import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { TextObject } from "./Models/TextModel";
import { RenderContent } from "./renderContent";

describe("test render function 'renderContent'", () => {
    interface Properties {
        textObject: TextObject | undefined;
    }

    const TestComponent = (props: Properties): JSX.Element => {
        return RenderContent(props.textObject);
    };

    const TestObject: TextObject = {
        items: [
            {
                id: "e7f4a895-b18c-4378-98b6-e85ef7c27e42",
                type: "html",
                value: "<p>Test line</p>",
                prop: "title",
                text: "",
            },
            {
                id: "b36cb3f6-673a-432c-8c65-b0c0a259f5ae",
                type: "html",
                value: "<p>Test line</p>",
                prop: "subtitle",
                text: "",
            },
            {
                id: "2a429fc0-099d-480e-bb08-5cbee9885597",
                type: "image",
                value: "image.jpg",
                prop: "",
                text: "",
            },
            {
                id: "cd75d329-1fce-4650-9870-455f37f5d6f4",
                type: "html",
                value: "<p>Test line</p>",
                prop: "dropcap",
                text: "",
            },
            {
                id: "47156d85-ed78-49e1-ad1a-20ca72efee5d",
                type: "html",
                value: "<p>Test line</p>",
                prop: "",
                text: "",
            },
            {
                id: "4191a77f-2618-4757-9e97-fedefc40748c",
                type: "separator",
                value: "",
                prop: "",
                text: "",
            },
            {
                id: "e40d98e8-8e66-4bc8-a170-675b7d8aaac9",
                type: "table",
                value: [
                    {
                        column0: "",
                        column1: "Header line",
                        column2: "Header line",
                    },
                    {
                        column0: "1",
                        column1: "value",
                        column2: "value",
                    },
                    {
                        column0: "2",
                        column1: "value",
                        column2: "value",
                    },
                    {
                        column0: "3",
                        column1: "value",
                        column2: "value",
                    },
                    {
                        column0: "4",
                        column1: "value",
                        column2: "value",
                    },
                ],
                prop: "",
                text: "",
            },
            {
                id: "7e2a6a15-2676-43ac-b2b0-f65297b90db2",
                type: "video",
                value: "cheers-commercial.mp4",
                prop: "cheers-commercial.png",
                text: "Video",
            },
            {
                id: "02bdf6d5-71c0-4a5d-ac4b-8f6700210b5e",
                type: "This is not valid object",
                value: "",
                prop: "",
                text: "",
            },
        ],
    };

    const noItems = shallow(<TestComponent textObject={undefined}></TestComponent>);
    const emptyItems = shallow(<TestComponent textObject={{ items: [] }}></TestComponent>);
    const withItems = shallow(<TestComponent textObject={TestObject}></TestComponent>);

    it("should return 'Cannot render content.' when called with items undefined.", () => {
        expect(noItems).toMatchSnapshot();
    });

    it("should return 'Cannot render content.' when called with empty array of items.", () => {
        expect(emptyItems).toMatchSnapshot();
    });

    it("should correctly render passed items.", () => {
        expect(withItems).toMatchSnapshot();
    });
});
