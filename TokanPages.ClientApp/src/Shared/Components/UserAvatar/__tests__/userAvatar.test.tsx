import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import UserAvatar from "../userAvatar";

describe("Test user avatar component.", () => 
{
    it("Should correctly render user avatar component view.", () => 
    {
        const tree = shallow(<UserAvatar 
            isLargeScale={true} 
            avatarName="test-avatar.png" 
            userLetter="T" 
        />);
        expect(tree).toMatchSnapshot();
    });
});
