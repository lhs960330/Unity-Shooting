/* 디자인 패턴 Object pool
 */

/* <오브젝트 풀>
 * 
 * 프로그램 내에서 비번하게 재활용되는 많은 수의 인스턴스들을 생성, 삭제를 하지않고
 * 미리 생성해놓은 인스턴스들을 오브젝트 풀에 보관하고
 * 인스턴스를 대여, 반납하는 방식으로 사용하는 기법
 * 
 * 구현
 * 1. 인스턴스들을 보관할 오브젝트 풀을 생성
 * 2. 프로그램(씬)의 시작시 오브젝트 풀에 인스턴스들을 생성하여 보관
 * 3. 인스턴스가 필요로 하는 상황에서 생성 대신 오브젝트 풀에서 인스턴스를 대여하여 사용
 * 4. 인스턴스가 필요로 하지 않는 상황에서 삭제 대신 오브젝트 풀에 반납하여 보관
 * 
 * 장점
 * 1. 빈번하게 사용하는 인스턴스의 생성에 소요되는 오버헤드를 줄임
 * 2. 빈번하게 사용하는 인스턴스의 삭제에 가비지 컬렉터의 부담을 줄임
 * (장점이 좋다고 맹목적으로 사용하지말자)
 * 
 * 단점(주의점)
 * 1. 미리 생성해놓은 인스턴스에 의해 사용하지 않는 경우에도 메모리를 차지하고 있음 (미리 만들어 놓고 보관하기 때문에 필요 없는 경우에도 생성해놓고 있어 메모리를 사용함)
 * 2. 메모리가 넉넉하지 않은 상황에서 너무 많은 오브젝트 풀링을 적용하는 경우,
 *    힙 영역의 여유공간이 줄어들어 오히려 가비지 컬렉터에 부담을 주어 프로그램이 느려지는 경우를 주의
 * 
 */

using System.Collections.Generic;
using UnityEngine;

namespace Digin
{
    public class ObjectPooler : MonoBehaviour
    {
        private PooledObject prefab;
        // 보관하기위해 Stack이나 Queue를 사용해준다. (취향 차이)
        private Stack<PooledObject> ObjectPool;
        // 몇개를 저장할지 정해둔다.
        private int poolSize = 100;

        // 저장해주는 함수
        public void CreatePool()
        {
            ObjectPool = new Stack<PooledObject>(poolSize);
            for (int i = 0; i < poolSize; i++)
            {
                PooledObject instance = Instantiate(prefab);
                // 비활성화 해서 저장해놓는다. (활성화해서 넣으면 게임씬에서 활용중이니 이런 짓을 할 이유가 없다.)
                instance.gameObject.SetActive(false);
                ObjectPool.Push(instance);
            }
        }

        // 꺼내주는 함수
        public PooledObject GetPool()
        {
            // 꺼내서 줄때 활성화 해준다.

            if (ObjectPool.Count > 0)
            {
                PooledObject instance = ObjectPool.Pop();
                instance.gameObject.SetActive(true);
                return instance;
            }
            // 내가 정한 크기 이상이면 더 만들어준다.
            else
            {
                return Instantiate(prefab);
            }
        }

        // 반납해주는 함수
        public void ReturnPool(PooledObject instance)
        {
            // 반납할때는 비활성화 시켜주고 넣어준다.

            if (ObjectPool.Count > poolSize)
            {
                instance.gameObject.SetActive(false);
                ObjectPool.Push(instance);
            }
            // 내가 정한 크기 이상으로 생성되었으면 삭제
            else
            {
                Destroy(instance);
            }
        }
    }

    public class PooledObject : MonoBehaviour
    {
        // 생성, 삭제가 빈번한 클래스
    }
}