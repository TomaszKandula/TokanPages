import "../../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { UserAvatarView } from "./userAvatarView";

describe("test user avatar component", () => {
    it("should correctly render large user avatar as user letter.", () => {
        const html = render(<UserAvatarView isLarge={true} userLetter={"T"} avatarSource={""} />);

        expect(html).toMatchSnapshot();
    });

    it("should correctly render small user avatar as user letter.", () => {
        const html = render(<UserAvatarView isLarge={false} userLetter={"T"} avatarSource={""} />);

        expect(html).toMatchSnapshot();
    });

    it("should correctly render large user avatar image.", () => {
        const html = render(
            <UserAvatarView
                isLarge={true}
                userLetter={"T"}
                avatarSource={"http://localhost/api/v1/assets/avatars/example_avatar.jpg"}
            />
        );

        expect(html).toMatchSnapshot();
    });

    it("should correctly render small user avatar image.", () => {
        const html = render(
            <UserAvatarView
                isLarge={false}
                userLetter={"T"}
                avatarSource={"http://localhost/api/v1/assets/avatars/example_avatar.jpg"}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
