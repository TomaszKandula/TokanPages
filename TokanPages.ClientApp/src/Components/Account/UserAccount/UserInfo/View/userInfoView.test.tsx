import "../../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { UserInfoView } from "./userInfoView";

describe("test account group component: userInfoView", () => {
    it("should render correctly '<UserInfoView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <UserInfoView
                    isLoading={false}
                    userStore={{
                        userId: "123456789",
                        isVerified: false,
                        aliasName: "Ester",
                        avatarName: "Ester.JPG",
                        firstName: "Ester",
                        lastName: "Exposito",
                        email: "exter.exposito@gmail.com",
                        shortBio: "Spanish Developer",
                        registered: "2020-01-01",
                        userToken: "123654789",
                        refreshToken: "951753789654123",
                        roles: [
                            {
                                id: "357159",
                                name: "Admin",
                                description: "System administrator",
                            },
                        ],
                        permissions: [
                            {
                                id: "951753",
                                name: "Access_All",
                            },
                        ],
                    }}
                    accountForm={{
                        firstName: "Ester",
                        lastName: "Exposito",
                        email: "ester.exposito@gmail.com",
                        userAboutText: "Spanish Developer",
                    }}
                    userImageName=""
                    isUserActivated={true}
                    isRequestingVerification={false}
                    formProgress={false}
                    keyHandler={jest.fn()}
                    formHandler={jest.fn()}
                    switchHandler={jest.fn()}
                    saveButtonHandler={jest.fn()}
                    verifyButtonHandler={jest.fn()}
                    sectionAccountInformation={{
                        caption: "USER DETAILS",
                        labelUserId: "User ID:",
                        labelEmailStatus: {
                            label: "Status",
                            negative: "Unverified",
                            positive: "Verified",
                        },
                        labelUserAlias: "User alias:",
                        labelFirstName: "First name:",
                        labelLastName: "last name:",
                        labelEmail: "Email:",
                        labelShortBio: "Bio:",
                        labelUserAvatar: "Avatar:",
                        updateButtonText: "Update",
                        uploadAvatarButtonText: "Upload",
                    }}
                    background={{ backgroundColor: "white" }}
                    userAbout={{
                        multiline: undefined,
                        minRows: undefined,
                    }}
                    fileUploadingCustomHandle="UserFile-Avatar"
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
