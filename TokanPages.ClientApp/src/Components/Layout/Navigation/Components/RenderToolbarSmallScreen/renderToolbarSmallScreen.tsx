import * as React from "react";
import { Link as RouterLink } from "react-router-dom";
import { NavigationViewProps, RenderLanguageListProps } from "../../Abstractions";
import { RenderMenuIcon } from "../RenderMenuIcon";
import { GET_FLAG_URL, GET_ICONS_URL } from "../../../../../Api";
import { LanguageItemDto } from "../../../../../Api/Models";
import { CustomImage, Icon, Link, Media, Skeleton } from "../../../../../Shared/Components";
import { APP_BAR_HEIGHT_DESKTOP, APP_BAR_HEIGHT_NON_DESKTOP_TOP } from "../../../../../Shared/constants";
import { RenderBottomSheet } from "../RenderBottomSheet";
import { RenderSelectionIcon } from "..";
import "./renderToolbarSmallScreen.css";
import { v4 as uuidv4 } from "uuid";

const listSeparator = (length: number, index: number): string => {
    return length === index + 1 ? "" : "line-separator";
};

const RenderDoubleToolbar = (props: NavigationViewProps) => (
    <div className="is-flex is-flex-direction-column is-flex-grow-1">
        <div
            className="is-flex is-justify-content-space-between is-align-items-center"
            style={{ height: APP_BAR_HEIGHT_NON_DESKTOP_TOP }}
        >
            <a
                className="bulma-navbar-start is-flex is-align-items-center ml-4 no-select"
                onClick={props.triggerBottomSheet}
            >
                <CustomImage
                    base={GET_FLAG_URL}
                    source={`${props.languageId}.png`}
                    title="Language flag"
                    alt={`A flag (${props.languageId}) for current language selection`}
                    className="bulma-image bulma-is-16x16"
                />
                <div className="has-text-black ml-2">{props.languageId?.toUpperCase()}</div>
                <Icon name="ChevronDown" size={1.4} className="ml-1" />
            </a>
            <Skeleton isLoading={props.isLoading} mode="Rect" height={24} width={175} className="mr-3">
                <Link
                    to={props.navigation?.signup?.link}
                    className="bulma-navbar-end is-flex is-align-items-center mr-4"
                >
                    <Icon name="PlusCircleOutline" size={1.2} className="mr-1" />
                    <p className="is-size-7 has-text-black">{props.navigation?.signup?.caption}</p>
                </Link>
            </Skeleton>
        </div>
        <hr className="line-separator" />
        <div
            className="is-flex is-justify-content-space-between is-align-items-center"
            style={{ height: APP_BAR_HEIGHT_DESKTOP }}
        >
            <div className="bulma-navbar-start">
                <Skeleton isLoading={props.isLoading} mode="Rect" height={24} width={24} className="ml-3">
                    <RenderMenuIcon {...props} />
                </Skeleton>
            </div>
            <div className="bulma-navbar-end mr-2">
                <Skeleton isLoading={props.isLoading} mode="Rect" height={30} width={180} className="mr-3">
                    <RouterLink
                        to={`/${props.languageId}`}
                        rel="noopener nofollow"
                        className="is-flex is-align-self-center"
                    >
                        <CustomImage
                            base={GET_ICONS_URL}
                            source={props.navigation?.logo}
                            title="TomKandula logo"
                            alt="An application logo"
                            width={180}
                            height={30}
                        />
                    </RouterLink>
                </Skeleton>
            </div>
        </div>
    </div>
);

const RenderLanguageList = (props: RenderLanguageListProps): React.ReactElement => {
    const baseClass = "navbar-list-item is-align-content-center";
    const length = props.languages?.languages.length;

    return (
        <>
            {props.languages?.languages.map((item: LanguageItemDto, index: number) => (
                <React.Fragment key={uuidv4()}>
                    <div
                        className={`${props.hasBulmaCells ? "bulma-cell line-separator" : ""} ${baseClass} ${!props.hasBulmaCells ? listSeparator(length, index) : ""}`}
                    >
                        <a
                            className="is-flex is-align-items-center is-justify-content-space-between"
                            onClick={() => props.languagePickHandler(item.id)}
                        >
                            <div className="is-flex is-align-items-center">
                                <CustomImage
                                    base={GET_FLAG_URL}
                                    source={`${item.id}.png`}
                                    title="Language flag"
                                    alt={`A flag (${item.name}) symbolizing available language`}
                                    className="bulma-image bulma-is-24x24 my-2 mx-0"
                                />
                                <h4 className="is-size-6 has-text-black has-text-weight-normal m-2 ml-4">
                                    {item.name}
                                </h4>
                            </div>
                            <RenderSelectionIcon
                                selection={item.id}
                                languageId={props.languageId}
                                size={1.5}
                                className="mr-1"
                            />
                        </a>
                    </div>
                </React.Fragment>
            ))}
        </>
    );
};

const RenderLanguages = (props: NavigationViewProps): React.ReactElement =>
    props.media.hasLandscape && props.media.isMobile ? (
        <div className="bulma-grid bulma-is-col-min-10 m-4">
            <RenderLanguageList {...props} hasBulmaCells />
        </div>
    ) : (
        <div className="is-flex is-flex-direction-column m-4">
            <RenderLanguageList {...props} />
        </div>
    );

export const RenderToolbarSmallScreen = (props: NavigationViewProps): React.ReactElement => (
    <>
        <Media.TabletOnly>
            <RenderDoubleToolbar {...props} />
            <RenderBottomSheet {...props}>
                <RenderLanguages {...props} />
            </RenderBottomSheet>
        </Media.TabletOnly>
        <Media.MobileOnly>
            <RenderDoubleToolbar {...props} />
            <RenderBottomSheet {...props}>
                <RenderLanguages {...props} />
            </RenderBottomSheet>
        </Media.MobileOnly>
    </>
);
