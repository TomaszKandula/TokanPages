import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { UserAvatarView } from "./userAvatarView";

describe("test user avatar component", () => 
{
    it("should correctly render large user avatar as user letter.", () => 
    {
        const tree = shallow(<UserAvatarView
            isLarge={true}
            userLetter={"T"}
            avatarSource={""}
        />);
        
        expect(tree).toMatchSnapshot();
    });

    it("should correctly render small user avatar as user letter.", () => 
    {
        const tree = shallow(<UserAvatarView
            isLarge={false}
            userLetter={"T"}
            avatarSource={""}
        />);
        
        expect(tree).toMatchSnapshot();
    });

    it("should correctly render large user avatar image.", () => 
    {
        const tree = shallow(<UserAvatarView
            isLarge={true}
            userLetter={"T"} 
            avatarSource={"http://localhost/api/v1/assets/avatars/example_avatar.jpg"}
        />);
        
        expect(tree).toMatchSnapshot();
    });

    it("should correctly render small user avatar image.", () => 
    {
        const tree = shallow(<UserAvatarView
            isLarge={false}
            userLetter={"T"}
            avatarSource={"http://localhost/api/v1/assets/avatars/example_avatar.jpg"}
        />);
        
        expect(tree).toMatchSnapshot();
    });
});
