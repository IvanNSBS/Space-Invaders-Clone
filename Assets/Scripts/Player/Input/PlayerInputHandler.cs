// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/Input/PlayerInputHandler.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Player.Input
{
    public class @PlayerInputHandler : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInputHandler()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputHandler"",
    ""maps"": [
        {
            ""name"": ""Actions"",
            ""id"": ""6c0cdb97-5cb9-47f5-bf29-61da7afccb5f"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""04effe15-19dd-4b6c-b2ad-76341130234b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""261d7df5-89d2-41bb-a042-1e9e281e552b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""SideMovement"",
                    ""id"": ""0a649693-c298-4764-9ae4-d0393e870b35"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""638aac74-ec6c-4a55-9492-a9e84d74ba9c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""dae461b8-065a-4b87-985c-d611ce36f922"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4a88a411-c079-457e-8c78-ba04f30acfbd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Actions
            m_Actions = asset.FindActionMap("Actions", throwIfNotFound: true);
            m_Actions_Move = m_Actions.FindAction("Move", throwIfNotFound: true);
            m_Actions_Shoot = m_Actions.FindAction("Shoot", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Actions
        private readonly InputActionMap m_Actions;
        private IActionsActions m_ActionsActionsCallbackInterface;
        private readonly InputAction m_Actions_Move;
        private readonly InputAction m_Actions_Shoot;
        public struct ActionsActions
        {
            private @PlayerInputHandler m_Wrapper;
            public ActionsActions(@PlayerInputHandler wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Actions_Move;
            public InputAction @Shoot => m_Wrapper.m_Actions_Shoot;
            public InputActionMap Get() { return m_Wrapper.m_Actions; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ActionsActions set) { return set.Get(); }
            public void SetCallbacks(IActionsActions instance)
            {
                if (m_Wrapper.m_ActionsActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMove;
                    @Shoot.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnShoot;
                    @Shoot.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnShoot;
                    @Shoot.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnShoot;
                }
                m_Wrapper.m_ActionsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Shoot.started += instance.OnShoot;
                    @Shoot.performed += instance.OnShoot;
                    @Shoot.canceled += instance.OnShoot;
                }
            }
        }
        public ActionsActions @Actions => new ActionsActions(this);
        public interface IActionsActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnShoot(InputAction.CallbackContext context);
        }
    }
}
