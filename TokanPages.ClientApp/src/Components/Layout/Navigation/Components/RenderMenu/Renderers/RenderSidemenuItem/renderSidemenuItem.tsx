import * as React from "react";
import { Icon, Link } from "../../../../../../../Shared/Components";
import { ItemDto, SubitemDto } from "../../../../../../../Api/Models";

const SidemenuWithSubitems = (props: ItemDto): React.ReactElement => (
    <li key={props.id}>
        <a className="is-flex has-background-white-ter">
            {/* <Icon name={props.icon as string} size={1} className="mr-2" /> */}
            <p>{props.value}</p>
        </a>
        <ul className="bulma-menu-list">
            {props.subitems?.map((item: SubitemDto, _index: number) => (
                <li key={item.id}>
                    <Link to={item.link as string} isDisabled={!item.enabled} className="is-flex">
                        <Icon name={item.icon as string} size={0.8} className="has-text-grey-dark mr-2" />
                        <span>{item.value}</span>
                    </Link>
                </li>
            ))}
        </ul>
    </li>
);

const SidemenuWithoutSubitems = (props: ItemDto): React.ReactElement => (
    <li key={props.id}>
        <Link to={props.link as string} isDisabled={!props.enabled} className="is-flex">
            <Icon name={props.icon as string} size={0.8} className="has-text-grey-dark mr-2" />
            <span>{props.value}</span>
        </Link>
    </li>
);

export const RenderSidemenuItem = (props: ItemDto): React.ReactElement => {
    const hasSubitems = props.subitems !== undefined && props.subitems.length > 0;
    return hasSubitems ? <SidemenuWithSubitems {...props} /> : <SidemenuWithoutSubitems {...props} />;
};
