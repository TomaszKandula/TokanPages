import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { AuthenticateUserResultDto } from "../../../../Api/Models";
import { ApplicationUserInfoView } from "../View/applicationUserInfoView";

describe("test view component for application user info", () => {
    it("should render correctly view component with passed props.", () => {
        const testData: AuthenticateUserResultDto = {
            userId: "5f4d15e2-0d32-4e20-b5f0-5a736152e993",
            isVerified: true,
            email: "happy@tester.com",
            aliasName: "tester",
            avatarName: "tester-avatar-icon.png",
            firstName: "Tester",
            lastName: "Testerovny",
            shortBio: "Happy tester working remotely",
            registered: "2020-01-01 15:02:03",
            userToken: "fbb220c9-7593-472d-bb95-7b41c436e332",
            refreshToken: "5f4d15e2-7593-4e20-b5f0-7b41c436e332",
            roles: [
                {
                    name: "EverydayUser",
                    description: "Standard access",
                    id: "f905ce01-c28f-4810-bb86-d29e8048f5b6",
                },
                {
                    name: "Tester",
                    description: "Can access test environments",
                    id: "52a520f3-9f60-4c76-80e3-28b9dcfe9bfb",
                },
            ],
            permissions: [
                {
                    name: "CanTestEverything",
                    id: "f34d2965-1caf-4c8b-8215-94d326d151d8",
                },
                {
                    name: "CanOrderPizza",
                    id: "198e0248-ade0-4868-b0f5-926dc5168a6e",
                },
            ],
        };

        const tree = shallow(<ApplicationUserInfoView state={true} data={testData} closeHandler={jest.fn()} />);

        expect(tree).toMatchSnapshot();
    });
});
