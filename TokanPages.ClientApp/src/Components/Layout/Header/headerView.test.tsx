import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { shallow } from "enzyme";
import { ApplicationDefault } from "../../../Store/Configuration";
import { HeaderView } from "./headerView";

describe("test component: headerView", () => {
    const testContent = {
        language: "eng",
        photo: "",
        caption: "Welcome to my web page",
        description: "I do programming for a living...",
        action: {
            text: "Read the story",
            href: "/action-link",
        },
    };
    
    let data = ApplicationDefault;
    data.contentHeader.content = testContent;

    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const wrapper = shallow(
        <div>
            <HeaderView />
        </div>
    );

    beforeEach(() => {
        useSelectorMock.mockClear();
        wrapper.find("HeaderView").dive();
    });

    it("should render correctly '<HeaderView />' when content is loaded.", () => {
        useSelectorMock.mockReturnValue(data);
        expect(wrapper).toMatchSnapshot();
    });
});
