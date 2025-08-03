import * as React from "react";
import { Link as RouterLink } from "react-router-dom";
import { ListSeparatorProps, NavigationViewProps } from "../../Abstractions";
import { RenderMenuIcon } from "../RenderMenuIcon";
import { GET_FLAG_URL, GET_ICONS_URL } from "../../../../../Api";
import { LanguageItemDto } from "../../../../../Api/Models";
import { CustomImage, Icon, Link, Media } from "../../../../../Shared/Components";
import { APP_BAR_HEIGHT_DESKTOP, APP_BAR_HEIGHT_NON_DESKTOP_TOP } from "../../../../../Shared/constants";
import { RenderBottomSheet } from "../RenderBottomSheet";
import { RenderSelectionIcon } from "..";
import "./renderToolbarSmallScreen.css";
import { v4 as uuidv4 } from "uuid";

const ListSeparator = (props: ListSeparatorProps): React.ReactElement | null => {
    return props.length === props.index + 1 ? null : <hr className="mx-0 my-1" />;
};

const RenderDoubleToolbar = (props: NavigationViewProps) => {
    return (
        <div className="is-flex is-flex-direction-column is-flex-grow-1">
            <div
                className="is-flex is-justify-content-space-between is-align-items-center"
                style={{ height: APP_BAR_HEIGHT_NON_DESKTOP_TOP }}
            >
                <a className="bulma-navbar-start is-flex is-align-items-center ml-4 no-select" onClick={props.triggerBottomSheet}>
                    <CustomImage
                        base={GET_FLAG_URL}
                        source={`${props.languageId}.png`}
                        title="Language flag"
                        alt={`A flag (${props.languageId}) for current language selection`}
                        className="bulma-image bulma-is-16x16"
                    />
                    <div className="has-text-black ml-2">{props.languageId?.toUpperCase()}</div>
                    <Icon name="ChevronDown" size={0.75} className="ml-1" />
                </a>
                <Link to={props.signup.link} className="bulma-navbar-end is-flex mr-4">
                    <Icon name="PlusCircleOutline" size={0.75} className="mr-1" />
                    <p className="is-size-7 has-text-black">{props.signup.caption}</p>
                </Link>
            </div>
            <hr className="navbar-top-section" />
            <div
                className="is-flex is-justify-content-space-between is-align-items-center"
                style={{ height: APP_BAR_HEIGHT_DESKTOP }}
            >
                <div className="bulma-navbar-start">{props.isLoading ? null : <RenderMenuIcon {...props} />}</div>
                <div className="bulma-navbar-end mr-2">
                    <RouterLink
                        to={`/${props.languageId}`}
                        rel="noopener nofollow"
                        className="is-flex is-align-self-center"
                    >
                        <CustomImage
                            base={GET_ICONS_URL}
                            source={props?.logo}
                            title="TomKandula logo"
                            alt="An application logo"
                            width={180}
                            height={30}
                        />
                    </RouterLink>
                </div>
            </div>
        </div>
    );
};

const RenderLanguages = (props: NavigationViewProps): React.ReactElement => (
    <>
        <div className="navbar-top-line"></div>
        <div className="is-flex is-flex-direction-column m-4">
            {props.languages?.languages.map((item: LanguageItemDto, index: number) => (
                <React.Fragment key={uuidv4()}>
                    <a className="is-flex is-align-items-center" onClick={() => props.languagePickHandler(item.id)}>
                        <CustomImage
                            base={GET_FLAG_URL}
                            source={`${item.id}.png`}
                            title="Language flag"
                            alt={`A flag (${item.name}) symbolizing available language`}
                            className="bulma-image bulma-is-16x16 m-2"
                        />
                        <div className="has-text-black m-2">{item.name}</div>
                        <div className="ml-4">
                            <RenderSelectionIcon selection={item.id} languageId={props.languageId} />
                        </div>
                    </a>
                    <ListSeparator length={props.languages?.languages.length} index={index} />
                </React.Fragment>
            ))}
        </div>
    </>
);

export const RenderToolbarSmallScreen = (props: NavigationViewProps): React.ReactElement => (
    <>
        <Media.TabletOnly>
            <RenderDoubleToolbar {...props} />
            <RenderBottomSheet {...props} bottomSheetHeight={350}>
                <RenderLanguages {...props} />
            </RenderBottomSheet>
        </Media.TabletOnly>
        <Media.MobileOnly>
            <RenderDoubleToolbar {...props} />
            <RenderBottomSheet {...props} bottomSheetHeight={350}>
                <RenderLanguages {...props} />
            </RenderBottomSheet>
        </Media.MobileOnly>
    </>
);
