using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject _parent;
    public GameObject _prev;
    public GameObject _next;
    public Vector3 __next;

    public bool _useNode;
    public float halfsize_1 = 0;
    public float halfsize_2 = 0;
    // Start is called before the first frame update
    void Start()
    {

        _useNode = false;
        halfsize_1 = Calcsize();
         //= longer;

        //Debug.Log(__next);
    }

    // Update is called once per frame
    void Update()
    {
        // 이전의 위치를 선형 보간으로 따라감. - x(크기)만큼 떨어져서
        if (_next != null && !_useNode 
            && !_parent.GetComponent<LinearInterpolation>().CheckUsingRope())
        {
            // 휘는 기능
            transform.position
                = Vector3.Lerp(transform.position, _next.GetComponent<Node>().GetPrev(), 0.5f);

            // 역동적이게 휘는 기능
            // transform.position = Vector3.Slerp(_prev.transform.position, _next.transform.position, 0.5f);
        }
        else if (!_useNode && _next != null)
        {
            transform.position 
                = Vector3.Lerp(_prev.transform.position, _next.transform.position, 0.5f);
        }
    }

    public Vector3 GetPrev()
    {
        return new Vector3(transform.position.x - halfsize_1, transform.position.y, transform.position.z );

    }


    public Vector3 GetNext()
    {
        // 안 쓸 예정
        return new Vector3(transform.position.x + halfsize_1, transform.position.y, transform.position.z );

    }

    public LinearInterpolation GetParent()
    {
        return _parent.GetComponent<LinearInterpolation>();
    }

    // 현재 노드가 사용중이라는 것을 알려주는 메서드
    public void GetNode()
    {
        _useNode = _useNode == true ? false : true;
    }
    

    // parent에 선택된 노드를 다시 넣어주는 메서드
    public void SetNode()
    {
        transform.SetParent(_parent.transform);
    }

    // 어떤 덩굴의 자식객체인지 세팅하는 메서드
    public void Setparent(GameObject parent)
    {
        _parent = parent;
    }

    // 현재 노드의 이전 노드를 세팅하는 메서드 (Head는 제외)
    public void SetPrev(GameObject prev)
    {
        _prev = prev;
    }

    // 현재 노드의 다음 노드를 세팅하는 메서드 (tail은 제외)
    public void SetNext(GameObject next)
    {
        _next = next;
    }

    public float Calcsize()
    {
        float _height = 0f;

        MeshFilter _mf = GetComponent<MeshFilter>();

        Vector3[] _vertices = _mf.mesh.vertices;

        foreach(var _vertice in _vertices)
        {
            
            Vector3 _pos = transform.TransformPoint(_vertice);

            if (_pos.y > _height)
            {
                _height = _pos.y;
            }
        }


        return _height;
    }

}
