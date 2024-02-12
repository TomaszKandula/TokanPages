import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { GetIcon } from "./getIcon";

describe("test render function 'getIcon'", () => {
    const NameList: string[] = [
        "Person",
        "PersonAdd",
        "Home",
        "ViewList",
        "Subject",
        "Build",
        "Assignment",
        "Star",
        "PhotoCamera",
        "SportsSoccer",
        "MusicNote",
        "DirectionsBike",
        "ContactMail",
        "Gavel",
        "Policy",
        "VpnKey",
        "Lock",
        "Unknown element renders apple icon",
    ];

    const wrapper = shallow(
        <div>
            {NameList.map((item, index) => (
                <GetIcon key={index} iconName={item} />
            ))}
        </div>
    );

    it("should render SVG icon based on input string.", () => {
        expect(wrapper).toMatchSnapshot();
    });
});
