/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID BUTTON_CLICK = 814543256U;
        static const AkUniqueID METEOR_EXPLODE = 809529563U;
        static const AkUniqueID METEOR_GRAB = 1149410026U;
        static const AkUniqueID METEOR_THROW = 3809331500U;
        static const AkUniqueID MUSIC_GAMEPLAY = 620878633U;
        static const AkUniqueID MUSIC_MENU = 1598298728U;
        static const AkUniqueID PLAYER_DAMAGED = 1386564838U;
        static const AkUniqueID PLAYER_DEATH = 3083087645U;
        static const AkUniqueID STATES_ENGINE = 2903516312U;
        static const AkUniqueID STATES_MUSIC = 2461894715U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace ENGINE
        {
            static const AkUniqueID GROUP = 268529915U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID STAGE1 = 936729722U;
                static const AkUniqueID STAGE2 = 936729721U;
                static const AkUniqueID STAGE3 = 936729720U;
                static const AkUniqueID STAGE4 = 936729727U;
            } // namespace STATE
        } // namespace ENGINE

        namespace MUSIC
        {
            static const AkUniqueID GROUP = 3991942870U;

            namespace STATE
            {
                static const AkUniqueID DEATH = 779278001U;
                static const AkUniqueID GAMEPLAY = 89505537U;
                static const AkUniqueID MENU = 2607556080U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace MUSIC

    } // namespace STATES

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
