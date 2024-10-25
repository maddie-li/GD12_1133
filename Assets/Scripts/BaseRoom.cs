using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BaseRoom : MonoBehaviour
    {
        //  LOCATION
        private bool[] _allowedDirections = new bool[4];

        public Vector2 Coords;

        // NAVIGATION
        private BaseRoom[] Exits { get; set; } = new BaseRoom[4];


        public BaseRoom NorthExit { get; set; } // directional linked rooms
        public BaseRoom EastExit { get; set; }
        public BaseRoom SouthExit { get; set; }
        public BaseRoom WestExit { get; set; }

        // FUNCTIONS



        public void CanExit(Direction dir, bool istrue) // sets if direction is allowed
        {
            if (istrue)
            {
                _allowedDirections[(int)dir] = true;
            }
            else
            {
                _allowedDirections[(int)dir] = false;

            }
            
        }

        public bool IsDirectionAllowed(Direction dir) // returns if diretion is allowed
        {
            return _allowedDirections[(int)dir];
        }

        public bool IsRoomAllowed(BaseRoom targetRoom, BaseRoom currentRoom)
        {
            for (int i = 0; i < Exits.Length; i++)
            {
                if (targetRoom == currentRoom.Exits[i])
                {
                    return true;
                }
            }

            return false;

        }

        public void SetRooms(BaseRoom northRoom, BaseRoom eastroom, BaseRoom southroom, BaseRoom westroom) // creates rooms
        {
            Exits[0] = northRoom;
            Exits[1] = eastroom;
            Exits[2] = southroom;
            Exits[3] = westroom;

        }

        // CONSTRUCTOR
        public BaseRoom (Vector2 coords)
        {
            this.Coords = coords;
        }


    }
}
