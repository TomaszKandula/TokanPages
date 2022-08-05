import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { UserAvatarView } from "./userAvatarView";

describe("Test user avatar component.", () => 
{
    it("Should correctly render user avatar component view.", () => 
    {
        const tree = shallow(<UserAvatarView 
            isLargeScale={true} 
            avatarName="test-avatar.png" 
            userLetter="T" 
        />);
        expect(tree).toMatchSnapshot();
    });
});
